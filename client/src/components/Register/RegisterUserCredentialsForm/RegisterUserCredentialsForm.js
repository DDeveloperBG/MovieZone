import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Form, Button } from "react-bootstrap";

import { auth } from "../../../utils/firebase";
import FormComponentError from "../../Shared/FormComponentError/FormComponentError";
import {
  checkIsUsernameUsed,
  registerUser,
} from "../../../services/authService";

import "./RegisterUserCredentialsForm.css";

function RegisterUserCredentialsForm() {
  const [usernameError, setUsernameError] = useState("");
  const [emailError, setEmailError] = useState("");
  const [passwordError, setPasswordError] = useState("");

  const navigate = useNavigate();

  const onSubmit = async (e) => {
    e.preventDefault();

    if (usernameError || emailError || passwordError) {
      return;
    }

    const username = e.target.username.value;
    const email = e.target.email.value;
    const password = e.target.password.value;

    const getMockEventObj = (value) => {
      return {
        target: {
          value: value,
        },
      };
    };

    try {
      usernameOnChange(getMockEventObj(username), true);
      emailOnChange(getMockEventObj(email), true);
      passwordOnChange(getMockEventObj(password), true);
      await usernameOnBlur(getMockEventObj(username), true);
    } catch {
      return;
    }

    auth
      .createUserWithEmailAndPassword(email, password)
      .then(async (userCredential) => {
        navigate("/register/payment/method");
        await registerUser(username);
        await userCredential.user.updateProfile({
          displayName: username,
        });
      })
      .catch((error) => {
        auth.signOut();
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

  const usernameOnChange = (e, shouldThrowError) => {
    const username = e.target.value;
    if (username === "") {
      setUsernameError("Username is required.");

      if (shouldThrowError) {
        throw new Error();
      }
    } else if (usernameError !== "Username is already used.") {
      setUsernameError("");
    }
  };

  const usernameOnBlur = async (e, shouldThrowError) => {
    const username = e.target.value;
    const isUsernameUsed = await checkIsUsernameUsed(username);

    if (isUsernameUsed) {
      setUsernameError("Username is already used.");

      if (shouldThrowError) {
        throw new Error();
      }
    } else if (usernameError === "Username is already used.") {
      setUsernameError("");
    }
  };

  const emailOnChange = (e, shouldThrowError) => {
    const email = e.target.value;
    if (email === "") {
      setEmailError("Email is required.");

      if (shouldThrowError) {
        throw new Error();
      }
    } else {
      setEmailError("");
    }
  };

  const passwordOnChange = (e, shouldThrowError) => {
    const password = e.target.value;
    if (password === "") {
      setPasswordError("Password is required.");

      if (shouldThrowError) {
        throw new Error();
      }
    } else {
      setPasswordError("");
    }
  };

  return (
    <Form id="register-form" className="p-3" onSubmit={onSubmit}>
      <Form.Group>
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
        <Form.Label>Username</Form.Label>
        <Form.Control
          type="text"
          name="username"
          placeholder="Username"
          onBlur={usernameOnBlur}
          onChange={usernameOnChange}
          autoComplete="false"
        ></Form.Control>
        <FormComponentError message={usernameError} />
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
