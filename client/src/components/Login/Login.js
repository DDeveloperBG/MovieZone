import { Container, Form, Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import { auth } from "../../utils/firebase";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import FormComponentError from "../Shared/FormComponentError/FormComponentError";
import "./Login.scss";

function Login() {
  const [emailError, setEmailError] = useState("");
  const [passwordError, setPasswordError] = useState("");

  const navigate = useNavigate();

  const onSubmit = (e) => {
    e.preventDefault();
    const email = e.target.email.value;
    const password = e.target.password.value;

    const getMockEventObj = (value) => {
      return {
        target: {
          value: value,
        },
      };
    };
    emailOnChange(getMockEventObj(e.target.email.value));
    passwordOnChange(getMockEventObj(e.target.password.value));

    if (emailError || passwordError) {
      return;
    }

    auth
      .signInWithEmailAndPassword(email, password)
      .then((_) => navigate("/"))
      .catch((error) => {
        console.log(error.code);

        switch (error.code) {
          case "auth/invalid-email":
            setEmailError("Invalid email.");
            break;
          case "auth/wrong-password":
            setPasswordError("Wrong password.");
            break;
          case "auth/user-not-found":
            setPasswordError("Wrong password.");
            break;
          default:
            return;
        }
      });
  };

  const emailOnChange = (e) => {
    const email = e.target.value;
    if (email === "") {
      setEmailError("Email is required.");
    } else {
      setEmailError("");
    }
  };

  const passwordOnChange = (e) => {
    const password = e.target.value;
    if (password === "") {
      setPasswordError("Password is required.");
    } else {
      setPasswordError("");
    }
  };

  return (
    <Container id="login-wrapper">
      <div
        id="content-wrapper"
        className="col-lg-5 col-md-6 col-sm-9 m-auto p-3"
      >
        <h1 className="text-center">
          <span className="colorful-font-style">Welcome back!</span> Sign in
        </h1>
        <Form id="login-form" className="p-3" onSubmit={onSubmit}>
          <Form.Group>
            <Form.Label>Email</Form.Label>
            <Form.Control
              type="email"
              name="email"
              placeholder="Enter Email"
              onChange={emailOnChange}
            ></Form.Control>
            <FormComponentError message={emailError} />
          </Form.Group>
          <Form.Group className="mt-3">
            <Form.Label>Password</Form.Label>
            <Form.Control
              type="password"
              name="password"
              placeholder="Enter Password"
              autoComplete="true"
              onChange={passwordOnChange}
            ></Form.Control>
            <FormComponentError message={passwordError} />
          </Form.Group>
          <Form.Group className="pt-2">
            <Button variant="primary" type="submit" className="rounded mt-3">
              Submit
            </Button>
          </Form.Group>
        </Form>
        <div className="ms-3">
          <Link to="/register/user/credentials">Don't have account?</Link>
        </div>
      </div>
    </Container>
  );
}

export default Login;
