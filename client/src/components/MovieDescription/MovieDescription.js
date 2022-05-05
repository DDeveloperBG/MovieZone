import { useSearchParams } from "react-router-dom";

import useFetchToGet from "../../hooks/useFetchToGet";

import MoviePoster from "./MoviePoster/MoviePoster";
import Spinner from "../Shared/Spinner/Spinner";

import "./MovieDescription.scss";

function MovieDescription() {
  const [searchParams] = useSearchParams();
  const id = searchParams.get("id");
  const [movieDetails, isLoading] = useFetchToGet(
    `${process.env.REACT_APP_MOVIE_API_GET_MOVIE_DETAILS_URL}?movieId=${id}`,
    true
  );

  if (isLoading) {
    return <Spinner />;
  }

  return (
    <div id="movie-description-wrapper">
      <MoviePoster id={id} {...movieDetails} />
      <div className="container mt-5">
        <h3 id="more-details-title">More Details</h3>
        <div>
          <h4>Cast</h4>
          <div className="row">
            {movieDetails.actorsNames.map((x) => (
              <span key={x} className="col-lg-2 col-md-3 col-sm-4 text-nowrap">
                {x}
              </span>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}

export default MovieDescription;
