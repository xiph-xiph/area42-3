import "./OrderConfirmationPage.css";

import { useNavigate } from "react-router-dom";

function OrderConfirmationPage() {
  const navigate = useNavigate();

  return (
    <main className="order-confirmation-page">
      <div className="order-confirmation-container">

        <h1>✅ Bedankt voor uw bestelling!</h1>

        <p className="confirmation-text">
          Uw bestelling is succesvol ontvangen.
        </p>

        <p className="confirmation-text">
          Wij gaan direct aan de slag zodat uw bestelling op tijd klaar staat.
        </p>

        <div className="confirmation-buttons">

          <button
            className="confirmation-button"
            onClick={() => navigate("/")}
          >
            Terug naar Home
          </button>

          <button
            className="confirmation-button secondary"
            onClick={() => navigate("/snackhoek/menu")}
          >
            Nog een bestelling plaatsen
          </button>

        </div>

      </div>
    </main>
  );
}

export default OrderConfirmationPage;