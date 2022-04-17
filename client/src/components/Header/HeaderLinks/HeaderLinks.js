import { Link } from "react-router-dom";
import { Navbar } from "react-bootstrap";
import "./HeaderLinks.css";

function HeaderLinks() {
  return (
    <Navbar.Collapse id="navbar-toggle-external-content">
      <ul className="navbar-nav mx-auto border-bottom border-lg-bottom-0 pt-2 pt-lg-0"></ul>
      <div className="d-flex py-3 py-lg-0">
        <Link
          id="login-btn"
          to="/login"
          className="btn text-1000 fw-medium order-1 order-lg-0"
        >
          Sign in
        </Link>
        <Link
          id="register-btn"
          to="/register"
          className="btn btn-outline-danger rounded-pill order-0"
        >
          Sign Up
        </Link>
      </div>
    </Navbar.Collapse>
  );
}

export default HeaderLinks;
