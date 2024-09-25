import { links } from "../../helpers/lists";

export default function LinkList() {
  return (
    <>
      {links.map((link, index) => (
        <li key={index}>
          <a href={link.url}>{link.name}</a>
        </li>
      ))}
    </>
  );
}
