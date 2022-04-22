import { Form, Button } from "react-bootstrap";
import { auth } from "../../../../utils/firebase";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import FormComponentError from "../../../Shared/FormComponentError/FormComponentError";
import "./RegisterUserCredentialsForm.css";

function RegisterUserCredentialsForm() {
  const [usernameError, setUsernameError] = useState("");
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
    usernameOnChange(getMockEventObj(e.target.username.value));
    emailOnChange(getMockEventObj(e.target.email.value));
    passwordOnChange(getMockEventObj(e.target.password.value));

    if (usernameError || emailError || passwordError) {
      return;
    }

    auth
      .createUserWithEmailAndPassword(email, password)
      .then((userCredential) => {
        const user = userCredential.user;
        console.log(user);
        navigate("/register/payment/method");
      })
      .catch((error) => {
        console.log(error.code);

        switch (error.code) {
          case "auth/email-already-in-use":
            setEmailError("Email already exists");
            break;
          case "auth/invalid-email":
            setEmailError("Invalid email.");
            break;
          case "auth/weak-password":
            setPasswordError("Password should be at least 6 symbols long.");
            break;
          default:
            return;
        }
      });
  };

  const usernameOnChange = (e) => {
    const username = e.target.value;
    if (username === "") {
      setUsernameError("Username is required.");
    } else {
      setUsernameError("");
    }
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
    <Form id="register-form" className="p-3" onSubmit={onSubmit}>
      <Form.Group>
        <Form.Label>Username</Form.Label>
        <Form.Control
          type="text"
          name="username"
          placeholder="Username"
          onChange={usernameOnChange}
        ></Form.Control>
        <FormComponentError message={usernameError} />
      </Form.Group>
      <Form.Group className="mt-3">
        <Form.Label>Email address</Form.Label>
        <Form.Control
          type="email"
          name="email"
          placeholder="Email"
          onChange={emailOnChange}
        ></Form.Control>
        <FormComponentError message={emailError} />
      </Form.Group>
      <Form.Group className="mt-3">
        <Form.Label>Password</Form.Label>
        <Form.Control
          type="password"
          name="password"
          placeholder="Add a Password"
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
  );
}

export default RegisterUserCredentialsForm;
