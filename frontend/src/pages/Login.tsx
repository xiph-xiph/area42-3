import "./login.css";

import { Link, useNavigate } from "react-router-dom";
import { useState } from "react";

import { login } from "../services/authService";

import area54Logo from "../assets/images/area54_logo.png";
import background from "../assets/images/area54_background.png";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const navigate = useNavigate();

  const handleLogin = async (e: React.SyntheticEvent) => {
    e.preventDefault();
    try {
      const response = await login(email, password);
      if (!response.success) {
        alert("Inloggen mislukt: " + response.message);
        return;
      }
      localStorage.setItem("token", response.token);
      alert("Inloggen succesvol!");
      navigate("/home");
    } catch (e) {
      console.error("Login error:", e);
      alert("Inloggen mislukt!");
    }
  };

  return (
    <div
      className="login-page"
      style={{ backgroundImage: `url(${background})` }}
    >
      <div className="login-card">
        <img src={area54Logo} alt="Area54 Logo" className="login-logo" />

        <h2>Welkom bij Area54</h2>

        <p className="login-subtitle">Log in om verder te gaan.</p>
        <form onSubmit={handleLogin}>
          <input
            type="email"
            placeholder="E-mailadres"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />

          <input
            type="password"
            placeholder="Wachtwoord"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />

          <button className="login-button" type="submit">
            Inloggen
          </button>
        </form>
        <p className="register-text">
          Nog geen account? <Link to="/register">Account aanmaken</Link>
        </p>
      </div>
    </div>
  );
};

export default Login;
