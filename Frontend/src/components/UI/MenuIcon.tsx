interface MenuItemProps {
  toggleMenu: () => void;
  textColor: string;
}
export default function MenuIcon({ toggleMenu, textColor }: MenuItemProps) {
  return (
    <div className="lg:hidden">
      <i
        className={`fa-solid fa-bars ${textColor}`}
        style={{ fontSize: "30px" }}
        onClick={toggleMenu}
      ></i>
    </div>
  );
}
