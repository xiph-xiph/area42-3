/* ========================================
   IMPORTS
======================================== */

import "./CheckoutPage.css";
import { useNavigate } from "react-router-dom";

/* ========================================
   COMPONENT
======================================== */

function CheckoutPage() {
    const navigate = useNavigate();
    return (
    <main className="checkout-page">
      <div className="checkout-container">

        {/* ========================================
            HEADER
        ======================================== */}

        <h1>🍟 Snackhoek</h1>

        <p className="checkout-subtitle">
          Controleer uw bestelling
        </p>

        {/* ========================================
            KLANTGEGEVENS
        ======================================== */}

        <section className="checkout-section">

          <h2>Persoonsgegevens</h2>

          <label>
            Naam
            <input
              type="text"
              placeholder="Uw naam"
            />
          </label>

          <label>
            Telefoonnummer
            <input
              type="tel"
              placeholder="06..."
            />
          </label>

        </section>

        {/* ========================================
            AFHAALTIJD
        ======================================== */}

        <section className="checkout-section">

          <h2>Afhaaltijd</h2>

          <label className="radio-option">
            <input
              type="radio"
              name="pickup"
              defaultChecked
            />
            Zo snel mogelijk
          </label>

          <label className="radio-option">
            <input
              type="radio"
              name="pickup"
            />
            Over 15 minuten
          </label>

          <label className="radio-option">
            <input
              type="radio"
              name="pickup"
            />
            Over 30 minuten
          </label>

          <label className="radio-option">
            <input
              type="radio"
              name="pickup"
            />
            Eigen tijd
          </label>

          <input
            type="time"
            className="time-input"
          />

        </section>

        {/* ========================================
            OPMERKINGEN
        ======================================== */}

        <section className="checkout-section">

          <h2>Opmerkingen</h2>

          <textarea
            rows={4}
            placeholder="Bijvoorbeeld: geen zout of extra mayonaise."
          />

        </section>

        {/* ========================================
            BESTELOVERZICHT
        ======================================== */}

        <section className="checkout-section">

          <h2>Besteloverzicht</h2>

          <div className="order-placeholder">

            Producten uit winkelwagen verschijnen hier.

          </div>

        </section>

        {/* ========================================
            BUTTON
        ======================================== */}

<button
  className="checkout-button"
  onClick={() => navigate("/snackhoek/bevestiging")}
>
  Bestelling plaatsen
</button>

      </div>
    </main>
  );
}



export default CheckoutPage;