import "./login.css";

import { Link, useNavigate } from "react-router-dom";

import area54Logo from "../assets/images/area54_logo.png";
import background from "../assets/images/area54_background.png";

const Login = () => {
  const navigate = useNavigate();

  const handleLogin = () => {
    navigate("/home");
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

        <input type="text" placeholder="Gebruikersnaam" />

        <input type="password" placeholder="Wachtwoord" />

        <button className="login-button" onClick={handleLogin}>
          Inloggen
        </button>

        <p className="register-text">
          Nog geen account? <Link to="/register">Account aanmaken</Link>
        </p>
      </div>
    </div>
  );
};

export default Login;
