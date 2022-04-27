import { Link } from "react-router-dom";
import { useContext } from "react";
import AuthContext from "../../../contexts/AuthContext";

function AuthLinks() {
  const { isAuthenticated, username } = useContext(AuthContext);

  let links;
  if (isAuthenticated) {
    links = (
      <>
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
