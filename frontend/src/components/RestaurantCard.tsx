
    
 type RestaurantCardProps = {
    name: string;
    image: string;
    logo: string;
    link: string;
  };
  
  function RestaurantCard(props: RestaurantCardProps) {
    return (
      <a href={props.link}>
        <article className="restaurant-card">
          <img className="restaurant-card__background" src={props.image} alt={props.name} />
          <img className="restaurant-card__logo" src={props.logo} alt={props.name} />
        </article>
      </a>
    );
  }
  
  export default RestaurantCard;
  
 