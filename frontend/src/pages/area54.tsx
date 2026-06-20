import "./area54.css";

import { Link } from "react-router-dom";

import area54Logo from "../assets/images/area54_logo.png";
import heroFoto54 from "../assets/images/area54_heropng.png";
import instagramLogo from "../assets/images/instagram-logo.png";
import background from "../assets/images/area54_background.png";
import ServiceCard from "../components/ServiceCard";
import FooterInfo from "../components/FooterInfo";
import famigliaLogo from "../assets/logos/frame_logo_lafamiglia.png";
import famigliaImage from "../assets/images/area54_lafamiglia_background.png";
import snackhoekLogo from "../assets/logos/frame_snackbar_logo.png";
import snackhoekImage from "../assets/images/snackhoek_hero.png";

const services = [
  {
    id: 1,
    logo: famigliaLogo,
    image: famigliaImage,
    route: "/restaurant",
  },
  {
    id: 2,
    logo: snackhoekLogo,
    image: snackhoekImage,
    route: "/snackhoek",
  },
];

const HomePageNew = () => {
  return (
    <div
      className="page-background"
      style={{ backgroundImage: `url(${background})` }}
    >
      <div className="area54-container">
        <div className="logo54-container">
          <img src={area54Logo} alt="logo" className="logo-area54" />
        </div>

        <div className="hero-container-area54">
          <img src={heroFoto54} className="herofoto-area54" alt="herofoto" />
        </div>

        <p className="welcome-text-area54">
          <h3>Welkom bij vakantiepark Area54!</h3>
          <br />
          De ideale bestemming om te ontspannen, plezier te maken en samen mooie
          herinneringen te creëren.
          <br />
          <br />
          Wij wensen je een fijn verblijf op ons park.
        </p>

        <div className="greenline"></div>
        <div className="text-area54">
          <h3>Restaurants</h3>
          <br />
          <p>
            Tijdens je verblijf op ons vakantiepark kun je genieten van diverse
            eetgelegenheden. Ontdek ons aanbod aan restaurants, snacks en
            drankjes en kies waar jij vandaag zin in hebt. We wensen je een
            smakelijk verblijf!
          </p>
        </div>
        <section className="restaurant-area54">
          {services.map((service) => (
            <ServiceCard
              key={service.id}
              logo={service.logo}
              image={service.image}
              route={service.route}
            />
          ))}
        </section>

        <div className="greenline"></div>
        <div className="text-parkonderhoud">
          <h3>Parkonderhoud</h3>
          <p>
            Zie je een storing of mankement tijdens je verblijf? Meld het
            eenvoudig via onderstaande pagina. Ons onderhoudsteam ontvangt je
            melding direct en doet er alles aan om het probleem zo snel mogelijk
            op te lossen.
          </p>

          <Link to="/parkonderhoud">
            <button className="button-onderhoud54">Meld een storing</button>
          </Link>
        </div>
        <div className="greenline"></div>
        <FooterInfo
          businessName="Area 54"
          address="Areastraat 12"
          postalCode="5678 AX"
          phone="0456-454637"
          openingDays="maandag t/m zondag"
          openingHours="08.00u. - 22.00u."
          socialLogo={instagramLogo}
        />
      </div>
    </div>
  );
};

export default HomePageNew;
