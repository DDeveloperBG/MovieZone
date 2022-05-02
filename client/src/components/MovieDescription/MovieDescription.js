import { useParams } from "react-router-dom";
import { Spinner } from "react-bootstrap";

import useFetchToGet from "../../hooks/useFetchToGet";

import MoviePoster from "./MoviePoster/MoviePoster";

import "./MovieDescription.scss";

function MovieDescription() {
  const { id } = useParams();
  const [movieDetails, isLoading] = useFetchToGet(
    `${process.env.REACT_APP_MOVIE_API_GET_MOVIE_DETAILS_URL}?movieId=${id}`,
    true
  );

  if (isLoading) {
    return <Spinner animation="border" className="ms-3 mt-5" />;
  } else {
    console.log(movieDetails);
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
