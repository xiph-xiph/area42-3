import "./HomePage.css";

import ServiceCard from "../components/ServiceCard";

import area54Logo from "../assets/logos/area54_logo.png";

import famigliaImage from "../assets/images/lafamiglia_background.png";
import snackhoekImage from "../assets/images/snackhoek_background_opacity_50.png";
import parkonderhoudImage from "../assets/images/maintenance_background.png";

import famigliaLogo from "../assets/logos/frame_logo_lafamiglia.png";
import snackhoekLogo from "../assets/logos/frame_snackbar_logo.png";
import parkonderhoudLogo from "../assets/logos/parkonderhoud_logo.png";



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
  {
    id: 3,
    logo: parkonderhoudLogo,
    image: parkonderhoudImage,
    route: "/parkonderhoud",
  },
];

const HomePage = () => {
  return (
    <main className="home">
      <img
        src={area54Logo}
        alt="Area54"
        className="home-logo"
      />

      <h1 className="home-title">
        Welkom
      </h1>

   
      <section className="home-services">
        {services.map((service) => (
          <ServiceCard
            key={service.id}
            logo={service.logo}
            image={service.image}
            route={service.route}
          />
        ))}
      </section>
    </main>
  );
};

export default HomePage;