import { Container, Form, Button } from "react-bootstrap";
import { Link } from "react-router-dom";
import "./Login.scss";

function Login() {
  return (
    <Container id="login-wrapper">
      <div
        id="content-wrapper"
        className="col-lg-5 col-md-6 col-sm-9 m-auto p-3"
      >
        <h1 className="text-center">
          <span className="colorful-font-style">Welcome back!</span> Sign in
        </h1>
        <Form id="login-form" className="p-3">
          <Form.Group>
            <Form.Label>Username / Email</Form.Label>
            <Form.Control
              type="text"
              placeholder="Enter Username or Email"
            ></Form.Control>
          </Form.Group>
          <Form.Group className="mt-3">
            <Form.Label>Password</Form.Label>
            <Form.Control
              type="password"
              placeholder="Enter Password"
            ></Form.Control>
          </Form.Group>
          <Form.Group className="pt-2">
            <Button variant="primary" type="submit" className="rounded mt-3">
              Submit
            </Button>
          </Form.Group>
        </Form>
        <div className="ms-3">
          <Link to="/">Don't have account?</Link>
        </div>
      </div>
    </Container>
  );
}

export default Login;
