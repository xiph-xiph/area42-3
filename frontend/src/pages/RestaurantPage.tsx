import "./RestaurantPage.css";
import FooterInfo from "../components/FooterInfo";

import DishCard from "../components/DishCard";

import heroImage from "../assets/images/famigilia_hero_image.png";
import logoImage from "../assets/logos/frame_logo_lafamiglia.png";

import pizzaImage from "../assets/images/pizza-margarita.png";
import pastaImage from "../assets/images/pasta_bolognese.png";
import lasagneImage from "../assets/images/lasagne.png";
import tiramisuImage from "../assets/images/tiramisu.png";
import instagramLogo from "../assets/images/instagram-logo.png";

import { Link } from "react-router-dom";


function RestaurantPage() {
  return (
    <main className="restaurant-page">
      <div className="restaurant-container">
        <section className="hero-section">
          <img
            src={heroImage}
            alt="La Famiglia"
            className="hero-image"
          />

          <img
            src={logoImage}
            alt="La Famiglia Logo"
            className="hero-logo"
          />

          <p className="hero-slogan">
            Samen genieten van Italië
          </p>
        </section>

        <section className="intro-section">
          <p className="intro-greeting">
            Hallo vakantieganger,
          </p>

          <p className="intro-text">
            La Famiglia is het Italiaanse familierestaurant van Het Eetplein.
            Geniet samen van verse pizza's, pasta's en Italiaanse klassiekers
            in een gezellige en ontspannen sfeer.
          </p>
        </section>

        <section className="button-section">
  <Link
    to="/menu"
    className="outline-button"
  >
    Menu
  </Link>

  <button className="outline-button">
    Reserveer
  </button>
</section>

        <section className="dishes-section">
          <h2>Populaire gerechten</h2>

          <div className="dishes-grid">
            <DishCard
              image={pizzaImage}
              title="Pizza Margarita"
            />

            <DishCard
              image={pastaImage}
              title="Pasta Bolognese"
            />

            <DishCard
              image={tiramisuImage}
              title="Tiramisu"
            />

            <DishCard
              image={lasagneImage}
              title="Lasagne"
            />
          </div>
        </section>

    
       
        <section className="reservation-banner">
  <p className="reservation-title">
    Samen eten tijdens je verblijf?
  </p>

  <p className="reservation-text">
    Reserveer snel en eenvoudig via onderstaande knop
  </p>

  <button className="reservation-button">
    Reserveer
  </button>
</section>
<FooterInfo
businessName="La Famiglia"
address="Areastraat 12"
postalCode="5678 AX"
phone="0456-454637"
openingDays="maandag t/m zondag"
openingHours="12.00u. - 22.00u."
socialLogo={instagramLogo}
/>
    </div>
    </main>
  );
}

export default RestaurantPage;