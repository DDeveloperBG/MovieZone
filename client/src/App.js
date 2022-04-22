import Header from "./components/Header/Header";
import { Routes, Route } from "react-router-dom";
import Home from "./components/Home/Home";
import Register from "./components/Register/RegisterUserCredentials/RegisterUserCredentials";
import RegisterPaymentMethod from "./components/Register/RegisterPaymentMethod/RegisterPaymentMethod";
import Login from "./components/Login/Login";

function App() {
  return (
    <div className="App">
      <Header></Header>
      <Routes>
        <Route exact path="/" element={<Home />} />
        <Route exact path="/login" element={<Login />} />
        <Route exact path="/register/user/credentials" element={<Register />} />
        <Route
          exact
          path="/register/payment/method"
          element={<RegisterPaymentMethod />}
        />
      </Routes>
    </div>
  );
}

export default App;
