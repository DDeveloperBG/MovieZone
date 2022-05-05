import { useState } from "react";
import { Form } from "react-bootstrap";
import ReactTagInput from "@pathofdev/react-tag-input";

import "@pathofdev/react-tag-input/build/index.css";
import "./AddMovieForm.scss";

function AddMovieForm() {
  const [categories, setCategories] = useState([]);
  const [actors, setActors] = useState([]);

  return (
    <div className="m-auto mb-6 col-lg-6">
      <h1>Add new movie</h1>
      <Form className="mt-3">
        <Form.Group className="form-group">
          <Form.Label>Movie name</Form.Label>
          <Form.Control
            name="name"
            type="text"
            placeholder="Enter movie name"
          />
        </Form.Group>
        <Form.Group className="form-group">
          <Form.Label>Movie File</Form.Label>
          <Form.Control name="listingImg" type="file" />
        </Form.Group>
        <Form.Group className="form-group">
          <Form.Label>Description</Form.Label>
          <Form.Control
            name="description"
            as="textarea"
            rows="2"
            placeholder="Enter description"
          />
        </Form.Group>
        <Form.Group className="form-group">
          <Form.Label>Listing Image</Form.Label>
          <Form.Control name="listingImg" type="file" />
        </Form.Group>
        <Form.Group className="form-group">
          <Form.Label>Details Image</Form.Label>
          <Form.Control name="detailsImg" type="file" />
        </Form.Group>
        <Form.Group className="form-group">
          <Form.Label>Year of publishing</Form.Label>
          <Form.Control
            name="yearOfPublishing"
            type="number"
            defaultValue={new Date().getFullYear()}
            min={1900}
            max={2099}
          />
        </Form.Group>
        <Form.Group className="form-group">
          <Form.Label>Age Restriction</Form.Label>
          <Form.Control name="ageRestriction" type="number" defaultValue={7} />
        </Form.Group>
        <Form.Group className="form-group">
          <Form.Label>Duration</Form.Label>
          <Form.Control
            name="duration"
            type="time"
            className="without-am-pm"
            min={"00:01"}
            max={"05:00"}
          />
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
    </div>
  );
}

export default AddMovieForm;
