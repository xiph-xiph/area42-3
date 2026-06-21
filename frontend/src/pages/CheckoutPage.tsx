/* ========================================
   IMPORTS
======================================== */

import timeStringToTodayDate from "../utils/timeStringToTodayDate";
import { checkoutCart } from "../services/orderService";
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
  type PickupTimes = "zsm" | "15m" | "30m" | "custom";
  const [pickupTime, setPickupTime] = useState<PickupTimes>("zsm");
  const [remarks, setRemarks] = useState("");
  const [customTime, setCustomTime] = useState<string>("");

  const { state } = useLocation();

  const cart = state?.cart ?? [];

  const totalPrice = cart.reduce(
    (total: number, item: { quantity: number; price: number }) =>
      total + item.quantity * item.price,
    0,
  );

  const handleOrder = async () => {
    let translatedPickupTime;
    switch (pickupTime) {
      case "zsm":
        translatedPickupTime = new Date();
        break;
      case "15m":
        translatedPickupTime = new Date(Date.now() + 15 * 60 * 1000);
        break;
      case "30m":
        translatedPickupTime = new Date(Date.now() + 30 * 60 * 1000);
        break;
      case "custom":
        translatedPickupTime = new Date(timeStringToTodayDate(customTime) ?? 0);
    }
    const response = await checkoutCart(
      name,
      phone,
      translatedPickupTime,
      remarks,
    );
    if (response.success) {
      navigate("/snackhoek/bevestiging");
    } else {
      alert("Fout bij plaatsen van bestelling: " + response.message);
    }
  };

  return (
    <main className="checkout-page">
      <div className="checkout-container">
        {/* ========================================
            HEADER
        ======================================== */}

        <h1>🍟 Snackhoek</h1>

        <p className="checkout-subtitle">Controleer uw bestelling</p>

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
              value="zsm"
              checked={pickupTime === "zsm"}
              onChange={(e) => setPickupTime(e.target.value as PickupTimes)}
            />
            Zo snel mogelijk
          </label>

          <label className="radio-option">
            <input
              type="radio"
              name="pickup"
              value="15m"
              checked={pickupTime === "15m"}
              onChange={(e) => setPickupTime(e.target.value as PickupTimes)}
            />
            Over 15 minuten
          </label>

          <label className="radio-option">
            <input
              type="radio"
              name="pickup"
              value="30m"
              checked={pickupTime === "30m"}
              onChange={(e) => setPickupTime(e.target.value as PickupTimes)}
            />
            Over 30 minuten
          </label>

          <label className="radio-option">
            <input
              type="radio"
              name="pickup"
              value="custom"
              checked={pickupTime === "custom"}
              onChange={(e) => setPickupTime(e.target.value as PickupTimes)}
            />
            Eigen tijd
          </label>

          <input
            type="time"
            className="time-input"
            value={customTime}
            onChange={(e) => setCustomTime(e.target.value)}
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
                        €{" "}
                        {(item.quantity * item.price)
                          .toFixed(2)
                          .replace(".", ",")}
                      </span>
                    </p>
                  ),
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

        <button className="checkout-button" onClick={handleOrder}>
          Bestelling plaatsen
        </button>
      </div>
    </main>
  );
}

export default CheckoutPage;
