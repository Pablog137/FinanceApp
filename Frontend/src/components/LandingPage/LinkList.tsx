import { links } from "../../helpers/links";

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
