import { useContext } from "react";

import Introduction from "./Introduction/Introduction";
import AuthContext from "../../contexts/AuthContext";

function Home() {
  const { isAuthenticated } = useContext(AuthContext);

  if (!isAuthenticated) {
    return (
      <div id="content-wrapper">
        <Introduction />
      </div>
    );
  }
}

export default Home;
