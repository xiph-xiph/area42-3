/* ========================================
   IMPORTS
======================================== */

import "./CheckoutPage.css";
import { useLocation, useNavigate } from "react-router-dom";

/* ========================================
   COMPONENT
======================================== */

function CheckoutPage() {
    const navigate = useNavigate();

    const { state } = useLocation();

const cart = state?.cart ?? [];

const totalPrice = cart.reduce(
  (total: number, item: { quantity: number; price: number }) =>
    total + item.quantity * item.price,
  0
);

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

{cart.length === 0 ? (
  <p>Geen producten geselecteerd.</p>
) : (
  <>
    {cart.map(
      (item: {
        id: number;
        title: string;
        quantity: number;
        price: number;
      }) => (
        <p key={item.id}>
          {item.title} × {item.quantity}
          <span style={{ float: "right" }}>
            € {(item.quantity * item.price)
              .toFixed(2)
              .replace(".", ",")}
          </span>
        </p>
      )
    )}

    <hr />

    <p>
      <strong>Totaal</strong>

      <strong style={{ float: "right" }}>
        € {totalPrice.toFixed(2).replace(".", ",")}
      </strong>
    </p>
  </>
)}

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