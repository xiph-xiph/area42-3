/* ========================================
   IMPORTS
======================================== */

import "./CheckoutPage.css";
import { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";

/* ========================================
   COMPONENT
======================================== */

function CheckoutPage() {
    const navigate = useNavigate();

    const [name, setName] = useState("");
const [phone, setPhone] = useState("");
const [pickupTime, setPickupTime] = useState("Zo snel mogelijk");
const [remarks, setRemarks] = useState("");

    const { state } = useLocation();

const cart = state?.cart ?? [];

const totalPrice = cart.reduce(
  (total: number, item: { quantity: number; price: number }) =>
    total + item.quantity * item.price,
  0
);


const handleOrder = () => {
    const order = {
      customer: {
        name,
        phone,
      },
  
      pickupTime,
  
      remarks,
  
      items: cart.map(
        (item: {
          id: number;
          quantity: number;
        }) => ({
          productId: item.id,
          quantity: item.quantity,
        })
      ),
    };
  
    console.log("Bestelling:", order);
  
    navigate("/snackhoek/bevestiging");
  };

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
  value={name}
  onChange={(e) => setName(e.target.value)}
/>
          </label>

          <label>
            Telefoonnummer
            <input
  type="tel"
  placeholder="06..."
  value={phone}
  onChange={(e) => setPhone(e.target.value)}
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
  value="Zo snel mogelijk"
  checked={pickupTime === "Zo snel mogelijk"}
  onChange={(e) => setPickupTime(e.target.value)}
/>
            Zo snel mogelijk
          </label>

          <label className="radio-option">
          <input
  type="radio"
  name="pickup"
  value="Zo snel mogelijk"
  checked={pickupTime === "Zo snel mogelijk"}
  onChange={(e) => setPickupTime(e.target.value)}
/>
            Over 15 minuten
          </label>

          <label className="radio-option">
          <input
  type="radio"
  name="pickup"
  value="Zo snel mogelijk"
  checked={pickupTime === "Zo snel mogelijk"}
  onChange={(e) => setPickupTime(e.target.value)}
/>
            Over 30 minuten
          </label>

          <label className="radio-option">
          <input
  type="radio"
  name="pickup"
  value="Zo snel mogelijk"
  checked={pickupTime === "Zo snel mogelijk"}
  onChange={(e) => setPickupTime(e.target.value)}
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
  value={remarks}
  onChange={(e) => setRemarks(e.target.value)}
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
  onClick={handleOrder}
>
  Bestelling plaatsen
</button>

      </div>
    </main>
  );
}



export default CheckoutPage;