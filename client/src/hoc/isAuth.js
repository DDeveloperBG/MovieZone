import { useContext } from "react";
import { Navigate } from "react-router-dom";
import AuthContext from "../contexts/AuthContext";

const isAuth = (
  WrappedComponent,
  expectedState = true,
  redirectPath = "/login"
) => {
  const Component = (props) => {
    const { isAuthenticated } = useContext(AuthContext);

    if (isAuthenticated !== expectedState) {
      return <Navigate to={redirectPath} />;
    }

    return <WrappedComponent {...props} />;
  };

  return <Component />;
};

export default isAuth;
