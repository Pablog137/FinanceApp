export default function Header() {
  return (
    <header className="col-span-6">
      <div className="col-span-12 flex items-center gap-5">
        <img
          src="/images/logo.svg"
          alt="image"
          style={{ width: "60px", height: "60px" }}
        />
        <h1 className="text-4xl text-white font-bold">FinanceApp</h1>
      </div>
    </header>
  );
}
