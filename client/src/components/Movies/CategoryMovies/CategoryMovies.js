import Movie from "../Movie/Movie";

function CategoryMovies({ name, movies }) {
  return (
    <div className="category-wrapper ms-3 mt-3">
      <h3>{name}</h3>
      <div className="row">
        {movies.map((x) => (
          <Movie
            key={x.id}
            id={x.id}
            name={x.name}
            description={x.description}
            imgUrl={x.listingImgUrl}
          />
        ))}
      </div>
    </div>
  );
}

export default CategoryMovies;
