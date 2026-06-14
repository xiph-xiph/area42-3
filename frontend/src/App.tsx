  import { Routes, Route } from "react-router-dom";

  import HomePage from "./pages/HomePage";
  import RestaurantPage from "./pages/RestaurantPage";
  import SnackhoekPage from "./pages/SnackhoekPage";
  import ParkonderhoudPage from "./pages/ParkOnderhoudpage";
  import MenuPage from "./pages/MenuPage";
  import ReservationPage from "./pages/ReservationPagse";
  import SnackMenuPage from "./pages/SnackMenuPage";


  function App() {
    return (
      <Routes>
        <Route path="/" element={<HomePage />} />

        <Route
          path="/restaurant"
          element={<RestaurantPage />}
        />

        <Route
          path="/snackhoek"
          element={<SnackhoekPage />}
        />

        <Route
          path="/snackhoek/menu"
          element={<SnackMenuPage />}
        />

        <Route
          path="/parkonderhoud"
          element={<ParkonderhoudPage />}
        />

        <Route path="/restaurant/menu"
        element={<MenuPage />}
        />

        <Route path="/restaurant/reserveren" 
        element={<ReservationPage />}
        />

      </Routes>
    );
  }

  export default App;