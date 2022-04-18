import { Link } from "react-router-dom";
import HeaderLinks from "./HeaderLinks/HeaderLinks";
import { Navbar, Container } from "react-bootstrap";

function Header() {
  return (
    <Navbar expand="lg">
      <Container>
        <Link to="/" className="text-decoration-none">
          <Navbar.Brand>
            <img src="assets/img/icon.png" alt="" width="55" />
            <span className="fs-1 ms-2 fw-medium">
              <span className="text-1000">Movie</span>
              <span className="fw-bold">Zone</span>
            </span>
          </Navbar.Brand>
        </Link>
        <Navbar.Toggle
          aria-controls="navbar-toggle-external-content"
          className="rounded-3"
        />
        <HeaderLinks />
      </Container>
    </Navbar>
  );
}

export default Header;
