import RestaurantCard from "../components/RestaurantCard";

import lafamigliaBackground from "../assets/images/lafamiglia_background.png";
import lafamigliaLogo from "../assets/images/frame_logo_lafamiglia.png";

import SnackHoekbackground from "../assets/images/snackbar_background.png";

import Snackhoeklogo from "../assets/images/frame_snackbar_logo.png";

import Area54Logo from "../assets/images/area54_logo.png";

import Area54background from "../assets/images/maintenance_background.png";


function Home() {
  const restaurants = [
    {
      name: "La Famiglia",
      image: lafamigliaBackground,
      logo: lafamigliaLogo,
      link: "/la-famiglia",
    },
    {
      name: "Snackhoek",
      image: SnackHoekbackground,
      logo: Snackhoeklogo,
      link: "/beach-club",
    },
    {
      name: "Area54",
      image: Area54background,
      logo: Area54Logo,
      link: "/pannenkoekenhuis",
    },
  ];

  return (
    <main>
      <h1>Restaurants</h1>

      <section className="restaurant-grid">
        {restaurants.map((restaurant) => (
          <RestaurantCard
            key={restaurant.name}
            name={restaurant.name}
            image={restaurant.image}
            logo={restaurant.logo}
            link={restaurant.link}
          />
        ))}
      </section>
    </main>
  );
}

export default Home;