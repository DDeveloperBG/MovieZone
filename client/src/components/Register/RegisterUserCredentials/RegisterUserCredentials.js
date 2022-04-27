import { Container, Row } from "react-bootstrap";
import { Link } from "react-router-dom";
import RegisterUserCredentialsForm from "../RegisterUserCredentialsForm/RegisterUserCredentialsForm";
import "./RegisterUserCredentials.scss";

function RegisterUserCredentials() {
  return (
    <Container id="registration-wrapper" className="mt-4">
      <div style={{ display: "flex" }}>
        <Row className="m-auto">
          <div id="registration-form-wrapper" className="col-lg-7 col-md-8">
            <h1 className="text-center">
              <span className="colorful-font-style">Welcome</span>, register for
              a few seconds and watch movies as long as you wish with whoever
              you like!
            </h1>
            <div className="col-lg-6 m-auto mt-4">
              <Row className="text-center">
                <h4 className="col-4">Sign up</h4>
                <span className="col-4 h4">
                  <Link to="/login">Sign in</Link>
                </span>
              </Row>
              <RegisterUserCredentialsForm />
            </div>
          </div>
          <div id="register-picture" className="col-lg-4 col-md-0 col-sm-0">
            <img
              src="https://mir-s3-cdn-cf.behance.net/project_modules/1400_opt_1/7561f2117973307.60800bc1ac13d.jpg"
              width="500px"
              height="500px"
              className="rounded-3 mt-3"
              alt=""
            />
          </div>
        </Row>
      </div>
    </Container>
  );
}

export default RegisterUserCredentials;
