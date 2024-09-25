interface MenuItemProps {
  toggleMenu: () => void;
}
export default function MenuIcon({ toggleMenu }: MenuItemProps) {
  return (
    <div className="lg:hidden">
      <i
        className="fa-solid fa-bars text-white"
        style={{ fontSize: "30px" }}
        onClick={toggleMenu}
      ></i>
    </div>
  );
}
