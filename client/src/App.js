import { Routes, Route, Navigate } from "react-router-dom";
import { useEffect, useState } from "react";

import Header from "./components/Header/Header";
import Home from "./components/Home/Home";
import Register from "./components/Register/RegisterUserCredentials/RegisterUserCredentials";
import RegisterPaymentMethod from "./components/Register/RegisterPaymentMethod/RegisterPaymentMethod";
import Login from "./components/Login/Login";
import Movies from "./components/Movies/Movies";
import MovieDescription from "./components/MovieDescription/MovieDescription";
import WatchMovie from "./components/WatchMovie/WatchMovie";
import AdminDashboard from "./components/Admin/AdminDashboard/AdminDashboard";
import AddMovie from "./components/Admin/AddMovie/AddMovie";
import VideoChat from "./components/VideoChat/VideoChat";

import AuthContext from "./contexts/AuthContext";

import { auth } from "./utils/firebase";

import isAuth from "./hoc/isAuth";
import isAdminHoc from "./hoc/isAdmin";

const roleClaim =
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

function App() {
  const [initializing, setInitializing] = useState(true);
  const [user, setUser] = useState(auth.currentUser);
  const [isAdmin, setIsAdmin] = useState(false);

  const onAuthStateChanged = (user) => {
    setUser(user);
    if (initializing) setInitializing(false);

    if (user) {
      auth.currentUser.getIdTokenResult().then((idTokenResult) => {
        setIsAdmin(idTokenResult.claims[roleClaim] === "Admin");
      });
    }
  };

  const onMount = () => {
    const subscriber = auth.onAuthStateChanged(onAuthStateChanged);
    return subscriber;
  };

  useEffect(onMount, []);

  if (initializing) return null;

  const authInfo = {
    isAuthenticated: Boolean(user),
    email: user?.email,
    username: user?.displayName,
    isAdmin: isAdmin,
  };

  return (
    <>
      <AuthContext.Provider value={authInfo}>
        <Header />
        <Routes>
          <Route exact path="/" element={<Home />} />
          <Route path="/login" element={isAuth(Login, false, "/")} />
          <Route
            path="/register/user/credentials"
            element={isAuth(Register, false, "/")}
          />
          <Route
            path="/register/payment/method"
            element={isAuth(RegisterPaymentMethod, false, "/")}
          />
          <Route
            path="/logout"
            element={isAuth(() => {
              auth.signOut();
              return <Navigate to="/" />;
            })}
          />
          <Route exact path="/movies" element={<Movies />} />
          <Route path="/movie/details/:id" element={isAuth(MovieDescription)} />
          <Route exact path="/movie/watch/:name" element={isAuth(WatchMovie)} />
          <Route path="/admin/dashboard" element={isAdminHoc(AdminDashboard)} />
          <Route path="/admin/addMovie" element={isAdminHoc(AddMovie)} />
          <Route path="/videoChat" element={isAuth(VideoChat)} />
        </Routes>
      </AuthContext.Provider>
    </>
  );
}

export default App;
