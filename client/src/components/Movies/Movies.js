import { useState } from "react";
import { useSearchParams } from "react-router-dom";

import CategoryMovies from "./CategoryMovies/CategoryMovies";
import CategoriesDropdown from "./CategoriesDropdown/CategoriesDropdown";
import CustomPagination from "../Shared/CustomPagination/CustomPagination";
import Spinner from "../Shared/Spinner/Spinner";

import useFetchToGet from "../../hooks/useFetchToGet";

function Movies() {
  const [searchParams, setSearchParams] = useSearchParams();

  const [categoryId, setCategoryId] = useState(
    searchParams.get("categoryId") ?? "default"
  );
  const [page, setPage] = useState(Number(searchParams.get("page") ?? 1));

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
    let selectedPage = Number(e.target?.textContent);
    if (Number.isNaN(selectedPage)) selectedPage = e;

    setPage(selectedPage);
    setSearchParams({
      categoryId: categoryId,
      page: selectedPage,
    });
  };

  const [categories, isLoadingCategories] = useFetchToGet(
    `${process.env.REACT_APP_MOVIES_CATEGORY_API_URL}`
  );

  const currentCategoryName = categories
    ? (categories.find(({ id }) => id === categoryId) ?? categories[0]).name
    : "Loading...";

  let categoriesDisplayComponent = undefined;
  if (isLoadingCategories) {
    categoriesDisplayComponent = <Spinner />;
  } else {
    categoriesDisplayComponent = (
      <CategoriesDropdown
        currentCategoryName={currentCategoryName}
        categories={categories}
        onCategorySelect={onCategorySelect}
      />
    );
  }

  const [moviesResponse, isLoadingMovies] = useFetchToGet(
    `${process.env.REACT_APP_MOVIE_API_GET_CATEGORY_MOVIES_URL}?categoryId=${categoryId}&page=${page}`
  );
  const { currentPageElements: movies, allPagesCount: allMoviesPagesCount } =
    moviesResponse ?? { currentPageElements: [], allPagesCount: 0 };

  let moviesDisplayComponent = undefined;
  if (isLoadingMovies) {
    moviesDisplayComponent = <Spinner />;
  } else {
    moviesDisplayComponent = (
      <CategoryMovies name={currentCategoryName} movies={movies} />
    );
  }

  return (
    <div id="movies-wrapper" className="container">
      {categoriesDisplayComponent}
      <br />
      {moviesDisplayComponent}

      <div className="ms-3 mt-2">
        <CustomPagination
          page={page}
          allPagesCount={allMoviesPagesCount}
          pageChangedHandler={pageChangedHandler}
        />
      </div>
    </div>
  );
}

export default Movies;
