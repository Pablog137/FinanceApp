import NavBar from "../components/Navbar";
import Header from "./Header";

export default function LandingPage() {
  return (
    <section className="grid grid-cols-12 p-6 ">
      <Header />
      <NavBar />
    </section>
  );
}
