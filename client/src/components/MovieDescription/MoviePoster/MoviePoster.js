import { Button } from "react-bootstrap";
import { Link } from "react-router-dom";

import "./MoviePoster.scss";

function MoviePoster({
  id,
  detailsImgUrl,
  name,
  yearOfPublishing,
  ageRestriction,
  hoursDuration,
  minutesDuration,
  description,
  actorsNames,
}) {
  return (
    <div id="movie-poster-wrapper" className="container">
      <div
        id="background-img"
        style={{ backgroundImage: `url(${detailsImgUrl})` }}
      ></div>
      <div
        id="description-text-wrapper"
        className="col-lg-6 col-md-8 col-sm-8 ps-3 pt-9 pb-7"
      >
        <h1 className="h2 font-white">{name}</h1>
        <p className="font-gray">
          <span>{yearOfPublishing}</span>
          <span className="ms-2 me-2">|</span>
          <span id="age-restriction-text">{ageRestriction}+</span>
          <span className="ms-2 me-2">|</span>
          <span>
            {hoursDuration}h {minutesDuration}m
          </span>
        </p>
        <p className="font-white col-7">{description}</p>
        <p>
          <span className="font-gray">Starring: </span>
          <span className="font-white">
            {actorsNames.slice(0, 3).join(", ")}
          </span>
        </p>
        <Link to={`/movie/watch/${name.replace(" ", "")}?id=${id}`}>
          <Button variant="danger" className="mt-3">
            Watch
          </Button>
        </Link>
      </div>
    </div>
  );
}

export default MoviePoster;
