import { Link } from "react-router-dom";
import { Navbar } from "react-bootstrap";

import AuthLinks from "../AuthLinks/AuthLinks";

import "./HeaderLinks.css";

function HeaderLinks() {
  return (
    <Navbar.Collapse id="navbar-toggle-external-content">
      <ul className="navbar-nav mx-auto border-bottom border-lg-bottom-0 pt-2 pt-lg-0"></ul>
      <AuthLinks />
    </Navbar.Collapse>
  );
}

export default HeaderLinks;
