import "./ReservationSuccess.css";

type ReservationSuccessProps = {
  onNewReservation: () => void;
};

function ReservationSuccess({
  onNewReservation,
}: ReservationSuccessProps) {
  return (
    <section className="reservation-success">
      <div className="success-icon">✓</div>

      <h2>Bedankt voor uw reservering!</h2>

      <p>
        Uw reserveringsaanvraag is succesvol verzonden.
      </p>

      <p>
        Wij nemen zo spoedig mogelijk contact met u op om uw reservering te
        bevestigen.
      </p>

      <button
        type="button"
        className="reservation-submit"
        onClick={onNewReservation}
      >
        Nieuwe reservering
      </button>
    </section>
  );
}

export default ReservationSuccess;