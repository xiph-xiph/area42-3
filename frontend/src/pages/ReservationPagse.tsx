import "./ReservationPage.css";

import ReservationForm from "../components/ReservationForm";
import BackButton from "../components/BackButton";

function ReservationPage() {
  return (
    <main className="reservation-page">
      <div className="reservation-container">
        <BackButton to="/restaurant" label="Terug naar restaurant" />

        <h1>Reserveer een tafel</h1>

        <p>Vul onderstaand formulier in om te reserveren.</p>

        <ReservationForm />
      </div>
    </main>
  );
}

export default ReservationPage;
