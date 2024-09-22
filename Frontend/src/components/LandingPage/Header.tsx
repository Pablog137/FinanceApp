import Logo from './Logo';
import NavBar from './Navbar';

export default function Header() {
  return (
    <section className="grid grid-cols-12 p-6 ">
      <Logo />
      <NavBar />
    </section>
  );
}
