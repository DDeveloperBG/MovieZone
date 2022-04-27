import { useState } from "react";
import { useSearchParams } from "react-router-dom";
import { Dropdown, ButtonGroup, Spinner } from "react-bootstrap";

import CategoryView from "./CategoryView/CategoryView";
import CustomPagination from "../Shared/CustomPagination/CustomPagination";

import useFetchToGet from "../../hooks/useFetchToGet";

function Movies() {
  const [searchParams, setSearchParams] = useSearchParams();

  const [categoryId, setCategoryId] = useState(searchParams.categoryId ?? null);
  const [page, setPage] = useState(Number(searchParams.page ?? 1));

  const [response, isLoading] = useFetchToGet(
    `${process.env.REACT_APP_MOVIES_CATEGORY_API_URL}?categoryId=${categoryId}&page=${page}`
  );

  if (isLoading) {
    return (
      <div id="movies-wrapper" className="container">
        <Spinner animation="border" className="ms-3 mt-5" />
      </div>
    );
  }

  const { categories, movies } = response;

  const currentCategoryName = (
    categories.find(({ id }) => id === categoryId) ?? categories[0]
  ).name;

  const onCategorySelect = (_, event) => {
    event.persist();
    const selectedCategoryName = event.target.innerText;
    if (selectedCategoryName !== currentCategoryName) {
      const categoryId = categories.find(
        ({ name }) => name === selectedCategoryName
      )?.id;

      if (categoryId !== undefined) {
        setCategoryId(categoryId);
        setSearchParams({
          categoryId: categoryId,
          page: 1,
        });
      }
    }
  };

  const pageChangedHandler = (e) => {
    const selectedPage = Number(e.target.textContent);
    setPage(selectedPage);
    setSearchParams({
      categoryId: categoryId,
      page: selectedPage,
    });
  };

  return (
    <div id="movies-wrapper" className="container">
      <Dropdown
        as={ButtonGroup}
        className="ms-3 mt-3"
        onSelect={onCategorySelect}
      >
        <Dropdown.Toggle className="btn btn-primary dropdown-toggle rounded-3 ps-3 pe-3">
          {currentCategoryName + " "}
        </Dropdown.Toggle>
        <Dropdown.Menu>
          {categories.map((x) => {
            if (x.name === currentCategoryName)
              return (
                <Dropdown.Item key={x.id} active>
                  {x.name}
                </Dropdown.Item>
              );
            return <Dropdown.Item key={x.id}>{x.name}</Dropdown.Item>;
          })}
        </Dropdown.Menu>
      </Dropdown>

      <CategoryView name={currentCategoryName} movies={movies} />

      <CustomPagination page={page} pageChangedHandler={pageChangedHandler} />
    </div>
  );
}

export default Movies;
