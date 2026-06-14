export type ReservationFormData = {
    name: string;
    email: string;
    phone: string;
    guests: string;
    date: string;
    time: string;
    remarks: string;
  };
  
  export type ReservationErrors = Record<string, string>;
  
  export function validateReservationForm(
    formData: ReservationFormData
  ): ReservationErrors {
    const errors: ReservationErrors = {};
  
    if (!formData.name.trim()) {
      errors.name = "Vul uw naam in.";
    }
  
    if (!formData.email.trim()) {
      errors.email = "Vul uw e-mailadres in.";
    } else if (!/\S+@\S+\.\S+/.test(formData.email)) {
      errors.email = "Voer een geldig e-mailadres in.";
    }
  
    if (!formData.phone.trim()) {
      errors.phone = "Vul uw telefoonnummer in.";
    }
  
    if (!formData.guests) {
      errors.guests = "Vul het aantal personen in.";
    } else if (Number(formData.guests) < 1) {
      errors.guests = "Minimaal 1 persoon.";
    }
  
    if (!formData.date) {
      errors.date = "Selecteer een datum.";
    }
  
    if (!formData.time) {
      errors.time = "Selecteer een tijd.";
    }
  
    return errors;
  }