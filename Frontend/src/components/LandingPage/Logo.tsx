export default function Logo({ textColor }: { textColor: string }) {
  return (
    <header className="col-span-4">
      <div className="col-span-12 flex items-center gap-5">
        <img
          src="/images/logo.svg"
          alt="image"
          style={{ width: "60px", height: "60px" }}
        />
        <h1 className={`${textColor} text-4xl  font-bold`}>FinanceApp</h1>
      </div>
    </header>
  );
}
