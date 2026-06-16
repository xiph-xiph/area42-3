import { Routes, Route } from "react-router-dom";

import HomePage from "./pages/HomePage";
import RestaurantPage from "./pages/RestaurantPage";
import SnackhoekPage from "./pages/SnackhoekPage";
import SnackMenuPage from "./pages/SnackMenuPage";
import CheckoutPage from "./pages/CheckoutPage";
import OrderConfirmationPage from "./pages/OrderConfirmationPage";
import Area54 from "./pages/area54";


import ParkonderhoudPage from "./pages/ParkOnderhoudpage";
import MenuPage from "./pages/MenuPage";
import ReservationPage from "./pages/ReservationPagse";

function App() {
  return (
    <Routes>
      <Route path="/" element={<Area54 />} />

      <Route
        path="/restaurant"
        element={<RestaurantPage />}
      />

      <Route
        path="/restaurant/menu"
        element={<MenuPage />}
      />

      <Route
        path="/restaurant/reserveren"
        element={<ReservationPage />}
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
        path="/snackhoek/checkout"
        element={<CheckoutPage />}
      />

      <Route
        path="/snackhoek/bevestiging"
        element={<OrderConfirmationPage />}
      />

      <Route
        path="/parkonderhoud"
        element={<ParkonderhoudPage />}
      />

<Route
        path="/area54"
        element={<Area54 />}
      />




    </Routes>
  );
}

export default App;