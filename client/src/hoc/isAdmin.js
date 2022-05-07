import { useContext } from "react";
import { Navigate } from "react-router-dom";

import AuthContext from "../contexts/AuthContext";

const isAdmin = (WrappedComponent, redirectPath = "/") => {
  const Component = (props) => {
    const { isAdmin } = useContext(AuthContext);

    if (!isAdmin) {
      return <Navigate to={redirectPath} />;
    }

    return <WrappedComponent {...props} />;
  };

  return <Component />;
};

export default isAdmin;
