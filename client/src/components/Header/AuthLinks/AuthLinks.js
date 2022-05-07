import { useContext } from "react";
import { Link } from "react-router-dom";
import { Button } from "react-bootstrap";

import AuthContext from "../../../contexts/AuthContext";

function AuthLinks() {
  const { isAuthenticated, isAdmin, username } = useContext(AuthContext);

  let adminDashboardLink;
  if (isAdmin) {
    adminDashboardLink = (
      <Link to="/admin/dashboard" className="mt-auto mb-auto me-3">
        <Button className="btn-sm" variant="secondary">
          Admin Dashboard
        </Button>
      </Link>
    );
  }

  let links;
  if (isAuthenticated) {
    links = (
      <>
        {adminDashboardLink}
        <p className="mt-auto mb-auto">Welcome, {username}!</p>
        <Link
          id="logout-btn"
          to="/logout"
          className="btn text-1000 fw-medium order-1 order-lg-0"
        >
          Sign out
        </Link>
      </>
    );
  } else {
    links = (
      <>
        <Link
          id="login-btn"
          to="/login"
          className="btn text-1000 fw-medium order-1 order-lg-0"
        >
          Sign in
        </Link>
        <Link
          id="register-btn"
          to="/register/user/credentials"
          className="btn btn-outline-danger rounded-pill order-0"
        >
          Sign Up
        </Link>
      </>
    );
  }

  return <div className="d-flex py-3 py-lg-0">{links}</div>;
}

export default AuthLinks;
