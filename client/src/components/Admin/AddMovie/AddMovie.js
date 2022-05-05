import AddMovieForm from "./AddMovieForm/AddMovieForm";
// TODO: Should add movie poster preview

function AddMovie() {
  return (
    <div id="add-movie-wrapper" className="container">
      <div className="row">
        <AddMovieForm />
      </div>
    </div>
  );
}

export default AddMovie;
