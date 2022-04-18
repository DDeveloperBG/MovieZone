import "./Introduction.scss";
import { Link } from "react-router-dom";

function Introduction() {
  return (
    <div id="introduction-wrapper">
      <div id="introduction-background"></div>
      <div className="pt-5">
        <table className="container p-5 card" id="introduction-text-wrapper">
          <tbody>
            <tr>
              <td className="col-md-8 col-lg-7">
                <h1 className="fw-medium">
                  Want to watch movies
                  <br />
                  together? Do it with{" "}
                  <span className="fw-bold">MovieZone.</span>
                </h1>
                <p className="mt-3 mb-4">
                  Now you can watch movies at the same time with your friends,
                  soul mate and so on. You can talk, pause, play, change the
                  scene and <strong id="enjoy-text">enjoy</strong>
                  <span id="blink-emojy">&#128521;</span>
                </p>
                <Link
                  to="/register"
                  className="btn btn-lg btn-danger hover-top btn-glow"
                  id="introduction-register-btn"
                >
                  Get Started
                </Link>
              </td>
              <td className="col-1"></td>
              <td className="col-md-4 col-lg-4 col-sm-0" id="couple-wrapper">
                <img
                  src="/assets/img/introduction-couple-img.jpg"
                  alt=""
                  id="couple"
                />
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default Introduction;
