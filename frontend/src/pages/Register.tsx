import "./login.css";
import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";

import { register } from "../services/authService";

import area54Logo from "../assets/images/area54_logo.png";
import background from "../assets/images/area54_background.png";

const Register = () => {
  const [name, setName] = useState("");
  const [phone, setPhone] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const navigate = useNavigate();

  const handleRegister = async (e: React.SyntheticEvent) => {
    e.preventDefault();
    if (password !== confirmPassword) {
      alert("Wachtwoorden komen niet overeen");
      return;
    }
    try {
      const response = await register(name, phone, email, password);
      if (!response.success) {
        alert("Registratie mislukt: " + response.message);
        return;
      }
      alert("Account succesvol aangemaakt!");
      navigate("/");
    } catch (error) {
      console.error("Error during registration:", error);
      alert("Er is een fout opgetreden bij het aanmaken van het account");
    }
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
        <form onSubmit={handleRegister}>
          <input
            type="text"
            placeholder="Voor- en achternaam"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />

          <input
            type="email"
            placeholder="E-mailadres"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />

          <input
            type="tel"
            placeholder="Telefoonnummer"
            value={phone}
            onChange={(e) => setPhone(e.target.value)}
          />

          <input
            type="password"
            placeholder="Wachtwoord"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />

          <input
            type="password"
            placeholder="Herhaal wachtwoord"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
          />

          <button
            className="login-button"
            onClick={handleRegister}
            type="submit"
          >
            Registreren
          </button>
        </form>
        <p className="register-text">
          Heb je al een account? <Link to="/">Inloggen</Link>
        </p>
      </div>
    </div>
  );
};

export default Register;
