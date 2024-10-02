import { navLinks } from "../../../helpers/lists";
import AsideElement from "./AsideElement";

export default function AsideElementList({ textColor }: { textColor: string }) {
  return (
    <>
      {navLinks.map((link, index) => (
        <AsideElement
          key={index}
          text={link.text}
          icon={link.icon}
          url={link.url}
          textColor={textColor}
        />
      ))}
    </>
  );
}
