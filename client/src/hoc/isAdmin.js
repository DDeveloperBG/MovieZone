import { useState, useEffect, useContext } from "react";
import { Navigate } from "react-router-dom";

import Spinner from "../components/Shared/Spinner/Spinner";

import { auth } from "../utils/firebase";

import AuthContext from "../contexts/AuthContext";

const roleClaim =
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

const isAdmin = (WrappedComponent, redirectPath = "/") => {
  const Component = (props) => {
    const [isAdmin, setIsAdmin] = useState(null);
    const { isAuthenticated } = useContext(AuthContext);

    useEffect(() => {
      if (!isAuthenticated) {
        setIsAdmin(false);
        return;
      }

      auth.currentUser.getIdTokenResult().then((idTokenResult) => {
        setIsAdmin(idTokenResult.claims[roleClaim] === "Admin");
      });
    }, []);

    if (isAdmin === null) {
      return <Spinner />;
    }

    if (!isAdmin) {
      return <Navigate to={redirectPath} />;
    }

    return <WrappedComponent {...props} />;
  };

  return <Component />;
};

export default isAdmin;
