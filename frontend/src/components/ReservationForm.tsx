import { useState } from "react";
import "./ReservationForm.css";

import ReservationSuccess from "./ReservationSuccess";

import { validateReservationForm } from "../utils/reservationValidation";
import type { ReservationFormData } from "../utils/reservationValidation";

const initialFormData: ReservationFormData = {
  name: "",
  email: "",
  phone: "",
  guests: "",
  date: "",
  time: "",
  remarks: "",
};

function ReservationForm() {
  const [formData, setFormData] = useState(initialFormData);

  const [errors, setErrors] = useState<Record<string, string>>({});

  const [isSubmitted, setIsSubmitted] = useState(false);

  const handleChange = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const { name, value } = event.target;

    setFormData((previousData) => ({
      ...previousData,
      [name]: value,
    }));

    setErrors((previousErrors) => ({
      ...previousErrors,
      [name]: "",
    }));
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const validationErrors = validateReservationForm(formData);

    if (Object.keys(validationErrors).length > 0) {
      setErrors(validationErrors);
      return;
    }

    console.log(formData);

    setIsSubmitted(true);

    setFormData(initialFormData);

    setErrors({});
  };

  const handleNewReservation = () => {
    setIsSubmitted(false);

    setFormData(initialFormData);

    setErrors({});
  };

  if (isSubmitted) {
    return <ReservationSuccess onNewReservation={handleNewReservation} />;
  }

  return (
    <form className="reservation-form" onSubmit={handleSubmit}>
      <div className="form-group">
        <label htmlFor="name">Naam</label>

        <input
          id="name"
          name="name"
          type="text"
          value={formData.name}
          onChange={handleChange}
          placeholder="Vul uw naam in"
        />

        {errors.name && <span className="form-error">{errors.name}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="email">E-mailadres</label>

        <input
          id="email"
          name="email"
          type="email"
          value={formData.email}
          onChange={handleChange}
          placeholder="voorbeeld@email.nl"
        />

        {errors.email && <span className="form-error">{errors.email}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="phone">Telefoonnummer</label>

        <input
          id="phone"
          name="phone"
          type="tel"
          value={formData.phone}
          onChange={handleChange}
          placeholder="06 12345678"
        />

        {errors.phone && <span className="form-error">{errors.phone}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="guests">Aantal personen</label>

        <input
          id="guests"
          name="guests"
          type="number"
          min="1"
          value={formData.guests}
          onChange={handleChange}
        />

        {errors.guests && <span className="form-error">{errors.guests}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="date">Datum</label>

        <input
          id="date"
          name="date"
          type="date"
          value={formData.date}
          onChange={handleChange}
        />

        {errors.date && <span className="form-error">{errors.date}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="time">Tijd</label>

        <input
          id="time"
          name="time"
          type="time"
          value={formData.time}
          onChange={handleChange}
        />

        {errors.time && <span className="form-error">{errors.time}</span>}
      </div>

      <div className="form-group">
        <label htmlFor="remarks">Opmerkingen</label>

        <textarea
          id="remarks"
          name="remarks"
          rows={5}
          value={formData.remarks}
          onChange={handleChange}
          placeholder="Eventuele wensen of opmerkingen..."
        />
      </div>

      <button type="submit" className="reservation-submit">
        Reservering aanvragen
      </button>
    </form>
  );
}

export default ReservationForm;
