import { Card, Button } from "react-bootstrap";
import { Link } from "react-router-dom";

function Movie({ id, name, description, imgUrl }) {
  return (
    <div className="col-lg-3 col-md-5 pb-3 pe-2">
      <Card className="movie-wrapper">
        <Card.Img
          variant="top"
          src={imgUrl}
          style={{ height: "300px", maxHeight: "300px" }}
        />
        <Card.Body>
          <Card.Title>{name}</Card.Title>
          <Card.Text>{description}</Card.Text>
          <Link to={`/movie/details/${id}`}>
            <Button variant="danger" className="rounded">
              More
            </Button>
          </Link>
        </Card.Body>
      </Card>
    </div>
  );
}

export default Movie;
