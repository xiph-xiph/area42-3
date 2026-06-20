import "./login.css";
import { Link, useNavigate } from "react-router-dom";

import area54Logo from "../assets/images/area54_logo.png";
import background from "../assets/images/area54_background.png";

const Register = () => {
  const navigate = useNavigate();

  const handleRegister = () => {
    alert("Account succesvol aangemaakt!");
    navigate("/");
  };

  return (
    <div
      className="login-page"
      style={{ backgroundImage: `url(${background})` }}
    >
      <div className="login-card">
        <img src={area54Logo} alt="Area54" className="login-logo" />

        <h2>Account aanmaken</h2>

        <p className="login-subtitle">
          Maak een account aan om gebruik te maken van Area54.
        </p>

        <input type="text" placeholder="Voor- en achternaam" />

        <input type="email" placeholder="E-mailadres" />

        <input type="text" placeholder="Gebruikersnaam" />

        <input type="password" placeholder="Wachtwoord" />

        <input type="password" placeholder="Herhaal wachtwoord" />

        <button className="login-button" onClick={handleRegister}>
          Registreren
        </button>

        <p className="register-text">
          Heb je al een account? <Link to="/">Inloggen</Link>
        </p>
      </div>
    </div>
  );
};

export default Register;
