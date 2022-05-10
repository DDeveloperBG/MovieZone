import { NavLink } from "react-router-dom";
import { Navbar } from "react-bootstrap";

import AuthLinks from "../AuthLinks/AuthLinks";

import "./HeaderLinks.scss";

function HeaderLinks() {
  return (
    <Navbar.Collapse id="navbar-toggle-external-content">
      <ul className="navbar-nav me-auto border-bottom border-lg-bottom-0 pt-2 pt-lg-0">
        <li className="nav-item">
          <NavLink to="/movies" className="nav-link h4 mb-0">
            Movies
          </NavLink>
        </li>
      </ul>
      <AuthLinks />
    </Navbar.Collapse>
  );
}

export default HeaderLinks;
