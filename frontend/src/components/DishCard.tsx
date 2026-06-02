
type DishCardProps = {
    image: string;
    name: string;
}

function DishCard (props: DishCardProps) {
    return (
        <article>
            <img src={props.image} alt="" />
            <h2>{props.name}</h2>
        </article>
    );
}

export default DishCard;