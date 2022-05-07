import { useState } from "react";
import { Form } from "react-bootstrap";
import { Formik, ErrorMessage } from "formik";
import { object, string, number } from "yup";
import ReactTagInput from "@pathofdev/react-tag-input";
import DurationPicker from "./DurationPicker/DurationPicker";

import FormComponentErrors from "../../../Shared/FormComponentErrors/FormComponentErrors";

import * as movieService from "../../../../services/movieService";

import "@pathofdev/react-tag-input/build/index.css";
import "./AddMovieForm.scss";

function getMinErrorMessage(fieldConstants) {
  return `${fieldConstants.label} min ${fieldConstants.unit} is ${fieldConstants.min}.`;
}

function getMaxErrorMessage(fieldConstants) {
  return `${fieldConstants.label} max ${fieldConstants.unit} is ${fieldConstants.max}.`;
}

function getFileFormatErrorMessage(fieldConstants) {
  return `${
    fieldConstants.label
  } can only be one of the following file formats: ${fieldConstants.allowedFormats.join(
    ", "
  )}.`;
}

function checkIsValidFileFormat(fieldConstants) {
  return (value) => {
    const { fileName } = value;
    if (!fileName) return false;

    const fileExtensionIndex = fileName.lastIndexOf(".");
    if (fileExtensionIndex === -1) return false;

    const fileExtension = fileName.substring(fileExtensionIndex + 1);
    return fieldConstants.allowedFormats.includes(fileExtension);
  };
}

const fieldsConstants = {
  name: {
    initialValue: "",
    label: "Name",
    max: 80,
    unit: "length",
  },
  description: {
    initialValue: "",
    label: "Description",
    min: 10,
    max: 250,
    unit: "length",
  },
  ageRestriction: {
    initialValue: 7,
    label: "Age restriction",
    min: 1,
    max: 21,
    unit: "age",
  },
  yearOfPublishing: {
    initialValue: new Date().getFullYear(),
    label: "Year of publishing",
    min: 1888,
    max: new Date().getFullYear() + 10,
    unit: "year",
  },
  duration: {
    initialValue: { hours: 0, minutes: 0 },
    label: "Duration",
  },
  movieFile: {
    initialValue: {},
    label: "Movie file",
    allowedFormats: ["mp4", "wmv", "webm"],
  },
  listingImg: {
    initialValue: {},
    label: "Listing image",
    allowedFormats: ["png", "jpg", "jpeg", "gif"],
  },
  detailsImg: {
    initialValue: {},
    label: "Details image",
    allowedFormats: ["png", "jpg", "jpeg", "gif"],
  },
};

function AddMovieForm() {
  const [categories, setCategories] = useState([]);
  const [actors, setActors] = useState([]);

  const getRequiredError = (fieldName) => {
    return `${fieldName} is required!`;
  };

  const movieSchema = object({
    name: string()
      .max(fieldsConstants.name.max, getMaxErrorMessage(fieldsConstants.name))
      .required(getRequiredError(fieldsConstants.name.label)),
    description: string()
      .min(
        fieldsConstants.description.min,
        getMinErrorMessage(fieldsConstants.description)
      )
      .max(
        fieldsConstants.description.max,
        getMaxErrorMessage(fieldsConstants.description)
      )
      .required(getRequiredError(fieldsConstants.description.label)),
    ageRestriction: number()
      .positive()
      .integer()
      .min(
        fieldsConstants.ageRestriction.min,
        getMinErrorMessage(fieldsConstants.ageRestriction)
      )
      .max(
        fieldsConstants.ageRestriction.max,
        getMaxErrorMessage(fieldsConstants.ageRestriction)
      )
      .required(getRequiredError(fieldsConstants.ageRestriction.label)),
    yearOfPublishing: number()
      .positive()
      .integer()
      .min(
        fieldsConstants.yearOfPublishing.min,
        getMinErrorMessage(fieldsConstants.yearOfPublishing)
      )
      .max(
        fieldsConstants.yearOfPublishing.max,
        getMaxErrorMessage(fieldsConstants.yearOfPublishing)
      )
      .required(getRequiredError(fieldsConstants.yearOfPublishing.label)),
    duration: object().test(
      "is-greater",
      `${fieldsConstants.duration.label} should be more than 0 minutes.`,
      (value) => value.minutes > 0 || value.hours > 0
    ),
    movieFile: object()
      .test(
        "is-allowed-format",
        getFileFormatErrorMessage(fieldsConstants.movieFile),
        checkIsValidFileFormat(fieldsConstants.movieFile)
      )
      .required(getRequiredError(fieldsConstants.movieFile.label)),
    listingImg: object()
      .test(
        "is-allowed-format",
        getFileFormatErrorMessage(fieldsConstants.listingImg),
        checkIsValidFileFormat(fieldsConstants.listingImg)
      )
      .required(getRequiredError(fieldsConstants.listingImg.label)),
    detailsImg: object()
      .test(
        "is-allowed-format",
        getFileFormatErrorMessage(fieldsConstants.detailsImg),
        checkIsValidFileFormat(fieldsConstants.detailsImg)
      )
      .required(getRequiredError(fieldsConstants.detailsImg.label)),
  });

  const onSubmit = (values) => {
    const movieFormData = new FormData();

    movieFormData.append("movieFile", values.movieFile.file);
    movieFormData.append("name", values.name);
    movieFormData.append("description", values.description);
    movieFormData.append("listingImg", values.listingImg.file);
    movieFormData.append("detailsImg", values.detailsImg.file);
    movieFormData.append("yearOfPublishing", values.yearOfPublishing);
    movieFormData.append("ageRestriction", values.ageRestriction);
    movieFormData.append("hoursDuration", values.duration.hours);
    movieFormData.append("minutesDuration", values.duration.minutes);
    categories.forEach((category) =>
      movieFormData.append("moviesCategoriesNames", category)
    );
    actors.forEach((actor) => movieFormData.append("actorsNames", actor));

    movieService.addMovie(movieFormData);
  };

  const initialValues = Object.entries(fieldsConstants).reduce(
    (accumulator, x) => {
      return { ...accumulator, [x[0]]: x[1].initialValue };
    },
    {}
  );

  const fileOnChange = (name, handleChange) => {
    return (e) => {
      handleChange({
        target: {
          name: name,
          value: {
            fileName: e.target.files[0].name,
            file: e.target.files[0],
          },
        },
      });
    };
  };

  return (
    <div className="m-auto mb-6 col-lg-6">
      <h1>Add new movie</h1>
      <Formik
        initialValues={initialValues}
        validationSchema={movieSchema}
        onSubmit={onSubmit}
        validateOnBlur
      >
        {({ values, handleChange, handleBlur, handleSubmit }) => (
          <Form
            className="mt-3"
            onSubmit={handleSubmit}
            encType="multipart/form-data"
          >
            <Form.Group className="form-group">
              <Form.Label>Movie name</Form.Label>
              <Form.Control
                name="name"
                type="text"
                placeholder="Enter movie name"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values.name}
              />
              <ErrorMessage name="name" component={FormComponentErrors} />
            </Form.Group>
            <Form.Group className="form-group">
              <Form.Label>Movie File</Form.Label>
              <Form.Control
                name="movieFile"
                type="file"
                onChange={fileOnChange("movieFile", handleChange)}
                onBlur={handleBlur}
              />
              <ErrorMessage name="movieFile" component={FormComponentErrors} />
            </Form.Group>
            <Form.Group className="form-group">
              <Form.Label>Description</Form.Label>
              <Form.Control
                name="description"
                as="textarea"
                rows="2"
                placeholder="Enter description"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values.description}
              />
              <ErrorMessage
                name="description"
                component={FormComponentErrors}
              />
            </Form.Group>
            <Form.Group className="form-group">
              <Form.Label>Listing Image</Form.Label>
              <Form.Control
                name="listingImg"
                type="file"
                onChange={fileOnChange("listingImg", handleChange)}
                onBlur={handleBlur}
              />
              <ErrorMessage name="listingImg" component={FormComponentErrors} />
            </Form.Group>
            <Form.Group className="form-group">
              <Form.Label>Details Image</Form.Label>
              <Form.Control
                name="detailsImg"
                type="file"
                onChange={fileOnChange("detailsImg", handleChange)}
                onBlur={handleBlur}
              />
              <ErrorMessage name="detailsImg" component={FormComponentErrors} />
            </Form.Group>
            <Form.Group className="form-group">
              <Form.Label>Year of publishing</Form.Label>
              <Form.Control
                name="yearOfPublishing"
                type="number"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values.yearOfPublishing}
              />
              <ErrorMessage
                name="yearOfPublishing"
                component={FormComponentErrors}
              />
            </Form.Group>
            <Form.Group className="form-group">
              <Form.Label>Age Restriction</Form.Label>
              <Form.Control
                name="ageRestriction"
                type="number"
                onChange={handleChange}
                onBlur={handleBlur}
                value={values.ageRestriction}
              />
              <ErrorMessage
                name="ageRestriction"
                component={FormComponentErrors}
              />
            </Form.Group>
            <Form.Group className="form-group">
              <Form.Label>Duration</Form.Label>
              <DurationPicker
                initialValue={values.duration}
                onChange={(duration) => {
                  handleChange({
                    target: {
                      name: "duration",
                      value: duration,
                    },
                  });
                }}
              />
              <ErrorMessage name="duration" component={FormComponentErrors} />
            </Form.Group>
            <Form.Group className="form-group">
              <Form.Label>Categories</Form.Label>
              <ReactTagInput
                tags={categories}
                onChange={setCategories}
                editable={true}
                readOnly={false}
                removeOnBackspace={true}
              />
            </Form.Group>
            <Form.Group className="form-group">
              <Form.Label>Actors</Form.Label>
              <ReactTagInput
                tags={actors}
                onChange={setActors}
                editable={true}
                readOnly={false}
                removeOnBackspace={true}
              />
            </Form.Group>
            <Form.Group className="mt-3 pt-2 col-lg-3 col-md-4 col-sm-4">
              <Form.Control type="submit" className="btn btn-primary" />
            </Form.Group>
          </Form>
        )}
      </Formik>
    </div>
  );
}

export default AddMovieForm;
