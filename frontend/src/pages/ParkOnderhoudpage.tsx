import "./ParkOnderhoudPage.css";
import { useState } from "react";

function ParkOnderhoudPage() {
  const [name, setName] = useState("");
  const [accommodation, setAccommodation] = useState("");
  const [phone, setPhone] = useState("");
  const [category, setCategory] = useState("Verlichting");
  const [urgency, setUrgency] = useState("Normaal");
  const [description, setDescription] = useState("");
  const [submitted, setSubmitted] = useState(false);

  const handleSubmit = () => {
    const report = {
      name,
      accommodation,
      phone,
      category,
      urgency,
      description,
    };

    console.log("Storing:", report);

    setSubmitted(true);
  };

  return (
    <main className="maintenance-page">
      <div className="maintenance-container">

        <h1>🔧 Parkonderhoud</h1>

        <p className="maintenance-subtitle">
          Heeft u een technische storing? Via het onderstaande formulier kun je eenvoudig een technische storing melden. Of het nu gaat om je accommodatie of een voorziening op het park, ons onderhoudsteam gaat er zo snel mogelijk mee aan de slag.
        </p>

        

        {submitted ? (
          <div className="maintenance-success">
            ✅ Uw melding is succesvol verzonden.
            <br />
            Onze technische dienst neemt deze zo snel mogelijk in behandeling.
          </div>
        ) : (
          <>
            <section className="maintenance-section">

              <label>
                Naam
                <input
                  type="text"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                />
              </label>

              <label>
                Accommodatienummer
                <input
                  type="text"
                  value={accommodation}
                  onChange={(e) => setAccommodation(e.target.value)}
                />
              </label>

              <label>
                Telefoonnummer
                <input
                  type="tel"
                  value={phone}
                  onChange={(e) => setPhone(e.target.value)}
                />
              </label>

              <label>
                Categorie
                <select
                  value={category}
                  onChange={(e) => setCategory(e.target.value)}
                >
                  
                  {/* opties */}
                  <option>Electriciteit</option>
                  <option>Internet</option>
                  <option>Ongedierte</option>
                </select>
              </label>

              <label>
                Omschrijving
                <textarea
                  rows={5}
                  value={description}
                  onChange={(e) => setDescription(e.target.value)}
                  placeholder="Beschrijf de storing..."
                />
              </label>

            </section>

            <section className="maintenance-section">

              <h2>Urgentie</h2>

              <label className="radio-option">
                <input
                  type="radio"
                  value="Laag"
                  checked={urgency === "Laag"}
                  onChange={(e) => setUrgency(e.target.value)}
                />
                Laag
              </label>

              <label className="radio-option">
                <input
                  type="radio"
                  value="Normaal"
                  checked={urgency === "Normaal"}
                  onChange={(e) => setUrgency(e.target.value)}
                />
                Normaal
              </label>

              <label className="radio-option">
                <input
                  type="radio"
                  value="Hoog"
                  checked={urgency === "Hoog"}
                  onChange={(e) => setUrgency(e.target.value)}
                />
                Hoog
              </label>

            </section>

            <button
              className="maintenance-button"
              onClick={handleSubmit}
            >
              Storing verzenden
            </button>
          </>
        )}

      </div>
    </main>
  );
}

export default ParkOnderhoudPage;