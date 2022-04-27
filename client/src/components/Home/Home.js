import { useContext } from "react";
import { Navigate } from "react-router-dom";

import Introduction from "./Introduction/Introduction";
import AuthContext from "../../contexts/AuthContext";

function Home() {
  const { isAuthenticated } = useContext(AuthContext);

  if (isAuthenticated) {
    return <Navigate to="/movies" />;
  }

  return (
    <div id="content-wrapper">
      <Introduction />
    </div>
  );
}

export default Home;
